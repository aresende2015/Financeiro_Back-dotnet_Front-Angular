import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { TiposdeinvestimentosComponent } from './tiposdeinvestimentos.component';
import { TiposdeinvestimentosListaComponent } from './tiposdeinvestimentos-lista/tiposdeinvestimentos-lista.component';
import { TiposdeinvestimentosDetalheComponent } from './tiposdeinvestimentos-detalhe/tiposdeinvestimentos-detalhe.component';

const routes: Routes = [
      {
        path: '', component: TiposdeinvestimentosComponent,
        children: [
          { path: 'detalhe/:id', component: TiposdeinvestimentosDetalheComponent },
          { path: 'detalhe', component: TiposdeinvestimentosDetalheComponent },
          { path: 'lista', component: TiposdeinvestimentosListaComponent }
        ]
      },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TiposdeinvestimentosRoutingModule { }
