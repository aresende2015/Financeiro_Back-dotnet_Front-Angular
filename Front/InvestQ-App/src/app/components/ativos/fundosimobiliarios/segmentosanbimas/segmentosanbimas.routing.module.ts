import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { SegmentosanbimasComponent } from './segmentosanbimas.component';
import { SegmentosanbimasDetalheComponent } from './segmentosanbimas-detalhe/segmentosanbimas-detalhe.component';
import { SegmentosanbimasListaComponent } from './segmentosanbimas-lista/segmentosanbimas-lista.component';

const routes: Routes = [
  {
    path: '', component: SegmentosanbimasComponent,
    children: [
      { path: 'detalhe/:id', component: SegmentosanbimasDetalheComponent },
      { path: 'detalhe', component: SegmentosanbimasDetalheComponent },
      { path: 'lista', component: SegmentosanbimasListaComponent }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SegmentosanbimasRoutingModule { }
