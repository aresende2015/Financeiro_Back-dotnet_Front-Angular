import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TaxadeirService } from '@app/services/ativos/taxadeir.service';
import { Guid } from 'guid-typescript';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { TaxaDeIR } from '@app/models/TaxaDeIR';

@Component({
  selector: 'app-taxasdeir-detalhe',
  templateUrl: './taxasdeir-detalhe.component.html',
  styleUrls: ['./taxasdeir-detalhe.component.scss']
})
export class TaxasdeirDetalheComponent implements OnInit {

  public taxaDeIR = {} as TaxaDeIR;

  taxaDeIRId: Guid;
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
              private taxaDeIRService: TaxadeirService,
              private spinner: NgxSpinnerService,
              private router: Router,
              private toastr: ToastrService)
  {
    this.localeService.use('pt-br');
  }

  ngOnInit(): void {
    this.carregarTaxaDeIR();
    this.validation();
    this.tipoDeAtivoOp = this.taxaDeIRService.getTipoDeAtivo();
    this.form.controls['dataFim'].disable();
  }

  public carregarTaxaDeIR(): void {

    if (this.activatedRouter.snapshot.paramMap.get('id') === null)
      this.taxaDeIRId = Guid.createEmpty();
    else {
      this.taxaDeIRId = Guid.parse(this.activatedRouter.snapshot.paramMap.get('id').toString());

      if (this.taxaDeIRId !== null && !this.taxaDeIRId.isEmpty()) {
        this.spinner.show();

        this.estadoSalvar = 'put';

        this.taxaDeIRService.getTaxaDeIRById(this.taxaDeIRId).subscribe({
          next: (_taxaDeIR: TaxaDeIR) => {
            this.taxaDeIR = {..._taxaDeIR};
            this.form.patchValue(this.taxaDeIR);
          },
          error: (error: any) => {
            this.spinner.hide();
            this.toastr.error('Erro ao tentar carregar a Taxa de IR.', 'Erro!');
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

      this.taxaDeIR = (this.estadoSalvar === 'post')
                      ? {...this.form.value}
                      : {id: this.taxaDeIR.id, ...this.form.value};

      this.taxaDeIRService[this.estadoSalvar](this.taxaDeIR).subscribe(
        (_taxaDeIR: TaxaDeIR) => {

          this.toastr.success('Taxa de IR salva com sucesso!', 'Sucesso');
          this.router.navigate([`taxasdeir/detalhe/${_taxaDeIR.id}`]);
        },
        (error: any) => {
          console.error(error);
          //this.spinner.hide();
          this.toastr.error('Erro ao atualizar a Taxa de IR', 'Erro');
        },
        () => {
          //this.spinner.hide();
        }
      ).add(() => {this.spinner.hide()});

    }

  }

}
