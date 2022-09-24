import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AdministradoresdefundosimobiliariosComponent } from './administradoresdefundosimobiliarios.component';
import { AdministradoresdefundosimobiliariosListaComponent } from './administradoresdefundosimobiliarios-lista/administradoresdefundosimobiliarios-lista.component';
import { AdministradoresdefundosimobiliariosDetalheComponent } from './administradoresdefundosimobiliarios-detalhe/administradoresdefundosimobiliarios-detalhe.component';

const routes: Routes = [
  {
    path: '', component: AdministradoresdefundosimobiliariosComponent,
    children: [
      { path: 'detalhe/:id', component: AdministradoresdefundosimobiliariosDetalheComponent },
      { path: 'detalhe', component: AdministradoresdefundosimobiliariosDetalheComponent },
      { path: 'lista', component: AdministradoresdefundosimobiliariosListaComponent }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdministradoresdefundosimobiliariosRoutingModule { }
