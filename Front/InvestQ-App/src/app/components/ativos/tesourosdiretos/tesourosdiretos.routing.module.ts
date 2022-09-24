import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { TesourosdiretosComponent } from './tesourosdiretos.component';
import { TesourosdiretosListaComponent } from './tesourosdiretos-lista/tesourosdiretos-lista.component';
import { TesourodiretoDetalheComponent } from './tesourodireto-detalhe/tesourodireto-detalhe.component';

const routes: Routes = [
  {
    path: '', component: TesourosdiretosComponent,
    children: [
      { path: 'detalhe/:id', component: TesourodiretoDetalheComponent },
      { path: 'detalhe', component: TesourodiretoDetalheComponent },
      { path: 'lista', component: TesourosdiretosListaComponent }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TesourosdiretosRoutingModule { }
