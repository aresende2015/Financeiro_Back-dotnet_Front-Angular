import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AcoesComponent } from './acoes.component';
import { AcoesDetalheComponent } from './acoes-detalhe/acoes-detalhe.component';
import { AcoesListaComponent } from './acoes-lista/acoes-lista.component';

const routes: Routes = [
  {
    path: '', component: AcoesComponent,
    children: [
      { path: 'detalhe/:id', component: AcoesDetalheComponent },
      { path: 'detalhe', component: AcoesDetalheComponent },
      { path: 'lista', component: AcoesListaComponent }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AcoesRoutingModule { }
