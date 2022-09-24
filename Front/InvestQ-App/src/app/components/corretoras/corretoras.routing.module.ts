import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CorretorasComponent } from './corretoras.component';
import { CorretoraListaComponent } from './corretora-lista/corretora-lista.component';
import { CorretoraDetalheComponent } from './corretora-detalhe/corretora-detalhe.component';

const routes: Routes = [
      {
        path: '', component: CorretorasComponent,
        children: [
          { path: 'detalhe/:id', component: CorretoraDetalheComponent },
          { path: 'detalhe', component: CorretoraDetalheComponent },
          { path: 'lista', component: CorretoraListaComponent }
        ]
      },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CorretorasRoutingModule { }
