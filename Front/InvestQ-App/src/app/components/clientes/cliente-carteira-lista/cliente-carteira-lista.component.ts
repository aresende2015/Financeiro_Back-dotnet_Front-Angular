import { Component, OnInit, TemplateRef } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Ativo } from '@app/models/Ativo';
import { Carteira } from '@app/models/Carteira';
import { Cliente } from '@app/models/Cliente';
import { TipoDeMovimentacao } from '@app/models/Enum/TipoDeMovimentacao.enum';
import { Lancamento } from '@app/models/Lancamento';
import { AtivoService } from '@app/services/ativos/ativo.service';
import { CarteiraService } from '@app/services/clientes/carteira.service';
import { ClienteService } from '@app/services/clientes/cliente.service';
import { LancamentoService } from '@app/services/clientes/lancamento.service';
import { Guid } from 'guid-typescript';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-cliente-carteira-lista',
  templateUrl: './cliente-carteira-lista.component.html',
  styleUrls: ['./cliente-carteira-lista.component.scss']
})
export class ClienteCarteiraListaComponent implements OnInit {

  modalRef?: BsModalRef;

  modalRef2?: BsModalRef;
  form!: FormGroup;

  get f(): any {
    return this.form.controls;
  }

  public getTipoDeMovimentacao() {
    return [
      {valor: TipoDeMovimentacao.Deposito, desc: 'Depósito' },
      {valor: TipoDeMovimentacao.Retirada, desc: 'Retirada' },
    ];
  }

  get bsConfig(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY',
      containerClass: 'theme-default',
      showWeekNumbers: false
    }
  }

  public carteiras: Carteira[] = [];
  public carteirasFiltrados: Carteira[] = [];
  public carteiraId = Guid.createEmpty();

  public lancamento = {} as Lancamento;
  public tipoDeMovimentacaoOp: any[];
  public ativo = {} as Ativo;

  clienteId: Guid;
  corretoraId: Guid;

  clienteNome: string;

  private _filtroLista: string = '';

  public get filtroLista(): string {
    return this._filtroLista;
  }

  public set filtroLista(value: string) {
    this._filtroLista = value;
    this.carteirasFiltrados = this.filtroLista
                              ? this.filtrarCarteiras(this.filtroLista)
                              : this.carteiras;
  }

  public onFiltroAcionado(evento: any) {
    this.filtroLista = evento.filtro;
  }

  filtrarCarteiras(filtrarPor: string): Carteira[] {
    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.carteiras.filter(
      (carteira: {descricao: string}) =>
        carteira.descricao.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  constructor(private carteiraService: CarteiraService,
              private clienteService: ClienteService,
              private ativoService: AtivoService,
              private lancamentoService: LancamentoService,
              private spinner: NgxSpinnerService,
              private modalService: BsModalService,
              private toastr: ToastrService,
              private fb: FormBuilder,
              private activatedRouter: ActivatedRoute,
              private router: Router) { }

  ngOnInit(): void {
    this.spinner.show();

    this.carregarCarteiras();
    this.bucarNomeDoCliente();

  }

  public bucarNomeDoCliente(): void {
    this.clienteService.getClienteById(this.clienteId).subscribe({
      next: (cliente: Cliente) => {
        this.clienteNome = cliente.nomeCompleto;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao tentar carregar o nome do cliente.', 'Erro!');
        console.error(error)
      },
      complete: () => {this.spinner.hide()}
    });
  }

  public carregarCarteiras(): void {

    if (this.activatedRouter.snapshot.paramMap.get('id') === null)
      this.clienteId = Guid.createEmpty();
    else {

      this.clienteId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('id').toString());
      //this.setorId = +this.activatedRouter.snapshot.paramMap.get('id')!;

      const observer = {
        next: (_carteiras: Carteira[]) => {
          this.carteiras = _carteiras;
          this.carteirasFiltrados = this.carteiras;
        },
        error: (error: any) => {
          this.toastr.error('Erro ao carregar a tela...', 'Error"');
        },
        complete: () => {}
      }

      this.carteiraService.getCarteirasByClienteId(this.clienteId).subscribe(observer).add(() => {this.spinner.hide()});

    }
  }

  openModal(event: any, template: TemplateRef<any>, carteiraId: Guid): void {
    event.stopPropagation();
    this.carteiraId = carteiraId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.carteiraService.deleteCarteira(this.carteiraId).subscribe(
      (result: any) => {
        if (result.message === 'Deletado') {
          this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
          //this.spinner.hide();
          this.carregarCarteiras();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error(`${error.error}`, 'Erro');
        //this.spinner.hide();
      },
      //() => {this.spinner.hide();}
    ).add(() => {this.spinner.hide();})
  }

  decline(): void {
    this.modalRef?.hide();
  }

  public editarCarteira(id: Guid): void {
    //alert(this.clienteId);
    //alert(id);
    this.router.navigate([`clientes/carteiradetalhe/${this.clienteId}/${id}`])  ;
  }

  /////////////////////////
  public carregarAtivo(): void {

    const observer = {
      next: (_ativo: Ativo) => {
        this.ativo = _ativo;
      },
      error: (error: any) => {
        this.spinner.hide();
        console.error(error);
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {this.spinner.hide()}
    }

    this.ativoService.getAtivoByCaixa().subscribe(observer);
  }

  public validation(): void {
    this.form = this.fb.group({
      valorDaOperacao: ['', [Validators.required]],
      dataDaOperacao: ['', [Validators.required]],
      quantidade: ['', [Validators.required]],
      tipoDeMovimentacao: [0, [Validators.required]],
      tipoDeOperacao: [0, [Validators.required]],
      codigoDoAtivo: [''],
      carteiraId: [null, [Validators.required]]
    });
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return {'is-invalid': campoForm.errors && campoForm.touched};
  }

  public resetForm(): void {
    this.form.reset();
  }

  public openModal2(event: any,
                   _template: TemplateRef<any>,
                   _carteiraId: Guid): void {

    event.stopPropagation();

    this.validation();

    this.tipoDeMovimentacaoOp = this.clienteService.getTipoDeMovimentacao();
    //this.form.controls['tipoDeMovimentacao'].setValue(tipoDeMovimentacao);
    this.carregarAtivo();
    this.form.controls['codigoDoAtivo'].setValue(this.ativo.codigoDoAtivo);
    this.form.controls['carteiraId'].setValue(_carteiraId);

    //this.form.controls['tipoDeMovimentacao'].disable();
    this.form.controls['codigoDoAtivo'].disable();

    this.modalRef2 = this.modalService.show(_template);
  }

  public cancelar(): void {
    this.modalRef2?.hide();
  }

  public confirmar(): void {
    this.spinner.show();

    this.lancamento.carteiraId = this.form.controls['carteiraId'].value;
    this.lancamento.valorDaOperacao = this.form.controls['valorDaOperacao'].value;
    this.lancamento.quantidade = this.form.controls['quantidade'].value;
    this.lancamento.dataDaOperacao = this.form.controls['dataDaOperacao'].value;
    this.lancamento.tipoDeMovimentacao = this.form.controls['tipoDeMovimentacao'].value;
    this.lancamento.ativoId = this.ativo.id;
    this.lancamento.tipoDeOperacao = 0;
    this.lancamento.tipoDeAtivo = this.ativo.tipoDeAtivo;

    this.modalRef2?.hide();

    this.spinner.show();

    this.lancamentoService.post(this.lancamento).subscribe(
      (_lancamento: Lancamento) => {
          this.toastr.success('Lançamento salvo com sucesso!', 'Sucesso');
          this.carregarCarteiras();
      },
      (error: any) => {
        console.error(error);
        this.toastr.error('Erro ao incluir o lançamento', 'Erro');
      },
    ).add(() => {this.spinner.hide();});
  }


}
