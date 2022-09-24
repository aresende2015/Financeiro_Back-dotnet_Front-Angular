import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LancamentosComponent } from './lancamentos.component';
import { LancamentoDetalheComponent } from './lancamento-detalhe/lancamento-detalhe.component';
import { LancamentosListaComponent } from './lancamentos-lista/lancamentos-lista.component';
import { LancamentoFiltroListaComponent } from './lancamento-filtro-lista/lancamento-filtro-lista.component';

const routes: Routes = [
      //{ path: 'lancamentos', redirectTo: 'lancamentos/filtro/lista' },
      //{ path: 'lancamentos/lista', redirectTo: 'lancamentos/filtro/lista' },
      {
        path: '', component: LancamentosComponent,
        children: [
          { path: 'filtro/lista', component: LancamentoFiltroListaComponent },
          { path: 'detalhe/:carteiraid/:id', component: LancamentoDetalheComponent },
          { path: 'detalhe/:carteiraid', component: LancamentoDetalheComponent },
          { path: 'detalhe', component: LancamentoDetalheComponent },
          { path: 'lista/:id', component: LancamentosListaComponent }
        ]
      },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LancamentosRoutingModule { }
