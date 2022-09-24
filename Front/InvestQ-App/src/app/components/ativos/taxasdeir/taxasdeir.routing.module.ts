import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { TaxasdeirComponent } from './taxasdeir.component';
import { TaxasdeirDetalheComponent } from './taxasdeir-detalhe/taxasdeir-detalhe.component';
import { TaxasdeirListaComponent } from './taxasdeir-lista/taxasdeir-lista.component';

const routes: Routes = [
  {
    path: '', component: TaxasdeirComponent,
    children: [
      { path: 'detalhe/:id', component: TaxasdeirDetalheComponent },
      { path: 'detalhe', component: TaxasdeirDetalheComponent },
      { path: 'lista', component: TaxasdeirListaComponent }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TaxasdeirRoutingModule { }
