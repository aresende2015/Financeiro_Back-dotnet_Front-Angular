import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { SetoresComponent } from './setores.component';
import { SetorDetalheComponent } from './setor-detalhe/setor-detalhe.component';
import { SetorListaComponent } from './setor-lista/setor-lista.component';
import { SubsetoresListaComponent } from './subsetores-lista/subsetores-lista.component';
import { SegmentosListaComponent } from './segmentos-lista/segmentos-lista.component';
import { SubsetorDetalheComponent } from './subsetor-detalhe/subsetor-detalhe.component';
import { SegmentoDetalheComponent } from './segmento-detalhe/segmento-detalhe.component';

const routes: Routes = [
  {
    path: '', component: SetoresComponent,
    children: [
      { path: 'detalhe/:id', component: SetorDetalheComponent },
      { path: 'subsetordetalhe/:id', component: SubsetorDetalheComponent },
      { path: 'listarsubsetores/:id', component: SubsetoresListaComponent },
      { path: 'listarsegmentos/:id', component: SegmentosListaComponent },
      { path: 'segmentodetalhe/:subsetorId/:id', component: SegmentoDetalheComponent },
      { path: 'detalhe', component: SetorDetalheComponent },
      { path: 'lista', component: SetorListaComponent }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SetoresRoutingModule { }
