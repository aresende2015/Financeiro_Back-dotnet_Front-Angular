import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { FundosimobiliariosComponent } from './fundosimobiliarios.component';
import { FundosimobiliariosDetalheComponent } from './fundosimobiliarios-detalhe/fundosimobiliarios-detalhe.component';
import { FundosimobiliariosListaComponent } from './fundosimobiliarios-lista/fundosimobiliarios-lista.component';

const routes: Routes = [
  {
    path: '', component: FundosimobiliariosComponent,
    children: [
      { path: 'detalhe/:id', component: FundosimobiliariosDetalheComponent },
      { path: 'detalhe', component: FundosimobiliariosDetalheComponent },
      { path: 'lista', component: FundosimobiliariosListaComponent }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FundosimobiliariosRoutingModule { }
