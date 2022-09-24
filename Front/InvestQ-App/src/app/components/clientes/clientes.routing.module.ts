import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ClientesComponent } from './clientes.component';
import { ClienteDetalheComponent } from './cliente-detalhe/cliente-detalhe.component';
import { ClienteListaComponent } from './cliente-lista/cliente-lista.component';
import { ClienteCarteiraDetalheComponent } from './cliente-carteira-detalhe/cliente-carteira-detalhe.component';
import { ClienteCarteiraListaComponent } from './cliente-carteira-lista/cliente-carteira-lista.component';

const routes: Routes = [
      //{ path: 'clientes', redirectTo: 'clientes/lista' },
      {
        path: '', component: ClientesComponent,
        children: [
          { path: 'detalhe/:id', component: ClienteDetalheComponent },
          { path: 'carteiradetalhe/:clienteid/:id', component: ClienteCarteiraDetalheComponent },
          { path: 'carteiradetalhe/:clienteid', component: ClienteCarteiraDetalheComponent },
          { path: 'carteiradetalhe', component: ClienteCarteiraDetalheComponent },
          { path: 'listarcarteiras/:id', component: ClienteCarteiraListaComponent },
          { path: 'detalhe', component: ClienteDetalheComponent },
          { path: 'lista', component: ClienteListaComponent }
        ]
      },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClientesRoutingModule { }
