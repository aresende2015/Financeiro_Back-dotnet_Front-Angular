import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TaxadenegociacaoService } from '@app/services/ativos/taxadenegociacao.service';
import { Guid } from 'guid-typescript';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { TaxaDeNegociacao } from './../../../../models/TaxaDeNegociacao';

@Component({
  selector: 'app-taxasdenegociacoes-detalhe',
  templateUrl: './taxasdenegociacoes-detalhe.component.html',
  styleUrls: ['./taxasdenegociacoes-detalhe.component.scss']
})
export class TaxasdenegociacoesDetalheComponent implements OnInit {

  public taxaDeNegociacao = {} as TaxaDeNegociacao;

  taxaDeNegociacaoId: Guid;
  form!: FormGroup;

  tipoDeAtivoOp: any[];

  estadoSalvar = 'post';

  get f(): any {
    return this.form.controls;
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

  get modoEditar(): boolean {
    return this.estadoSalvar === 'put';
  }

  constructor(private fb: FormBuilder,
              private activatedRouter: ActivatedRoute,
              private localeService: BsLocaleService,
              private taxaDeNegociacaoService: TaxadenegociacaoService,
              private spinner: NgxSpinnerService,
              private router: Router,
              private toastr: ToastrService)
  {
    this.localeService.use('pt-br');
  }

  ngOnInit(): void {
    this.carregarTaxaDeNegociacao();
    this.validation();
    this.tipoDeAtivoOp = this.taxaDeNegociacaoService.getTipoDeAtivo();
    this.form.controls['dataFim'].disable();
  }

  public carregarTaxaDeNegociacao(): void {

    if (this.activatedRouter.snapshot.paramMap.get('id') === null)
      this.taxaDeNegociacaoId = Guid.createEmpty();
    else {
      this.taxaDeNegociacaoId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('id').toString());

      if (this.taxaDeNegociacaoId !== null && !this.taxaDeNegociacaoId.isEmpty()) {
        this.spinner.show();

        this.estadoSalvar = 'put';

        this.taxaDeNegociacaoService.getTaxaDeNegociacaoById(this.taxaDeNegociacaoId).subscribe({
          next: (_taxaDeNegociacao: TaxaDeNegociacao) => {
            this.taxaDeNegociacao = {..._taxaDeNegociacao};
            this.form.patchValue(this.taxaDeNegociacao);
          },
          error: (error: any) => {
            this.spinner.hide();
            this.toastr.error('Erro ao tentar carregar o Provento.', 'Erro!');
            console.error(error);
          },
          complete: () => {this.spinner.hide()}
        });
      }
    }
  }


  public validation(): void {
    this.form = this.fb.group({
      tipoDeAtivo: [false, [Validators.required]],
      percentual: ['', [Validators.required]],
      dataInicio: [null, Validators.required],
      dataFim: [null]
    });
  }

  public resetForm(): void {
    this.form.reset();
  }

  public onClicouEm(evento) {
    if (evento.botaoClicado === 'cancelar') {
      this.resetForm();
    } else {
      this.salvarAlteracao();
    }
  }

  public cssValidator(campoForm: FormControl | AbstractControl): any {
    return {'is-invalid': campoForm.errors && campoForm.touched};
  }

  public salvarAlteracao(): void {
    this.spinner.show();

    if (this.form.valid) {

      this.taxaDeNegociacao = (this.estadoSalvar === 'post')
                      ? {...this.form.value}
                      : {id: this.taxaDeNegociacao.id, ...this.form.value};

      this.taxaDeNegociacaoService[this.estadoSalvar](this.taxaDeNegociacao).subscribe(
        (_taxaDeNegociacao: TaxaDeNegociacao) => {

          this.toastr.success('Taxa de Negociação salva com sucesso!', 'Sucesso');
          this.router.navigate([`taxasdenegociacoes/detalhe/${_taxaDeNegociacao.id}`]);
        },
        (error: any) => {
          console.error(error);
          //this.spinner.hide();
          this.toastr.error('Erro ao atualizar a Taxa de Negociação', 'Erro');
        },
        () => {
          //this.spinner.hide();
        }
      ).add(() => {this.spinner.hide()});

    }

  }

}
