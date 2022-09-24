import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { TaxasdenegociacoesComponent } from './taxasdenegociacoes.component';
import { TaxasdenegociacoesDetalheComponent } from './taxasdenegociacoes-detalhe/taxasdenegociacoes-detalhe.component';
import { TaxasdenegociacoesListaComponent } from './taxasdenegociacoes-lista/taxasdenegociacoes-lista.component';

const routes: Routes = [
  {
    path: '', component: TaxasdenegociacoesComponent,
    children: [
      { path: 'detalhe/:id', component: TaxasdenegociacoesDetalheComponent },
      { path: 'detalhe', component: TaxasdenegociacoesDetalheComponent },
      { path: 'lista', component: TaxasdenegociacoesListaComponent }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TaxasdenegociacoesRoutingModule { }
