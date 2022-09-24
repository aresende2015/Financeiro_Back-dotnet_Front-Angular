import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ProventosComponent } from './proventos.component';
import { ProventosDetalheComponent } from './proventos-detalhe/proventos-detalhe.component';
import { ProventosListaComponent } from './proventos-lista/proventos-lista.component';

const routes: Routes = [
  {
    path: '', component: ProventosComponent,
    children: [
      { path: 'detalhe/:id', component: ProventosDetalheComponent },
      { path: 'detalhe', component: ProventosDetalheComponent },
      { path: 'lista', component: ProventosListaComponent }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProventosRoutingModule { }
