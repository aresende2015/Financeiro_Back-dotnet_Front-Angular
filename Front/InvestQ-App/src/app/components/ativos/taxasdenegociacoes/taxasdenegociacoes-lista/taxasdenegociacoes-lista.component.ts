import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { TaxaDeNegociacao } from '@app/models/TaxaDeNegociacao';
import { TaxadenegociacaoService } from '@app/services/ativos/taxadenegociacao.service';
import { Guid } from 'guid-typescript';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { TipoDeAtivo } from '@app/models/Enum/TipoDeAtivo.enum';

@Component({
  selector: 'app-taxasdenegociacoes-lista',
  templateUrl: './taxasdenegociacoes-lista.component.html',
  styleUrls: ['./taxasdenegociacoes-lista.component.scss']
})
export class TaxasdenegociacoesListaComponent implements OnInit {

  modalRef?: BsModalRef;

  public taxasDeNegociacoes: TaxaDeNegociacao[] = [];
  public taxaDeNegociacaoId = Guid.createEmpty();

  constructor(
    private taxaDeNegociacaoService: TaxadenegociacaoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.carregarTaxasDeNegociacoes();

    setTimeout(() => {
      /** spinner ends after 5 seconds */
      //this.spinner.hide();
    }, 3000);
  }

  public carregarTaxasDeNegociacoes(): void {
    this.spinner.show();
    const observer = {
      next: (_taxasDeNegociacoes: TaxaDeNegociacao[]) => {
        this.taxasDeNegociacoes = _taxasDeNegociacoes;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao carregar a tela...', 'Error"');
      },
      complete: () => {this.spinner.hide()}
    }

    this.taxaDeNegociacaoService.getAllTaxasDeNegociacoes().subscribe(observer);
  }

  public openModal(event: any, template: TemplateRef<any>, taxaDeNegociacaoId: Guid): void {
    event.stopPropagation();
    this.taxaDeNegociacaoId = taxaDeNegociacaoId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  public confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.taxaDeNegociacaoService.deleteTaxaDeNegociacao(this.taxaDeNegociacaoId).subscribe(
      (result: any) => {
        if (result.message === 'Deletado') {
          this.toastr.success('O registro foi excluído com sucesso!', 'Excluído!');
          //this.spinner.hide();
          this.carregarTaxasDeNegociacoes();
        }
      },
      (error: any) => {
        console.error(error);
        this.toastr.error(`Erro ao tentar deletar o provento ${this.taxaDeNegociacaoId}`, 'Erro');
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
