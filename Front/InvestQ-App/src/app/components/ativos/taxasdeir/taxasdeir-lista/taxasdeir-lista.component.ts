import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { TaxaDeIR } from '@app/models/TaxaDeIR';
import { TaxadeirService } from '@app/services/ativos/taxadeir.service';
import { Guid } from 'guid-typescript';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { TipoDeAtivo } from '@app/models/Enum/TipoDeAtivo.enum';


@Component({
  selector: 'app-taxasdeir-lista',
  templateUrl: './taxasdeir-lista.component.html',
  styleUrls: ['./taxasdeir-lista.component.scss']
})
export class TaxasdeirListaComponent implements OnInit {

  modalRef?: BsModalRef;

  public taxasDeIR: TaxaDeIR[] = [];
  public taxaDeIRId = Guid.createEmpty();

  constructor(
    private taxaDeIRService: TaxadeirService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.carregarTaxasDeIR();

    setTimeout(() => {
      /** spinner ends after 5 seconds */
      //this.spinner.hide();
    }, 3000);
  }

  public carregarTaxasDeIR(): void {
    this.spinner.show();
    const observer = {
      next: (_taxasDeIR: TaxaDeIR[]) => {
        this.taxasDeIR = _taxasDeIR;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {this.spinner.hide()}
    }

    this.taxaDeIRService.getAllTaxasDeIR().subscribe(observer);
  }

  public openModal(event: any, template: TemplateRef<any>, taxaDeIRId: Guid): void {
    event.stopPropagation();
    this.taxaDeIRId = taxaDeIRId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  public confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.taxaDeIRService.deleteTaxaDeIR(this.taxaDeIRId).subscribe(
      (result: any) => {
        if (result.message === 'Deletado') {
          this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
          //this.spinner.hide();
          this.carregarTaxasDeIR();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error(`Erro ao tentar deletar a Taxa de IR ${this.taxaDeIRId}`, 'Erro');
        //this.spinner.hide();
      },
      //() => {this.spinner.hide();}
    ).add(() => {this.spinner.hide();});
  }

  public decline(): void {
    this.modalRef?.hide();
  }

  public pegaTipoDeAtivo(tipoDeAtivo: TipoDeAtivo): string {
    return TipoDeAtivo[tipoDeAtivo];
  }

}
