import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { PortifoliosComponent } from './portifolios.component';
import { PortifolioFiltroListaComponent } from './portifolio-filtro-lista/portifolio-filtro-lista.component';
import { PortifoliosListaComponent } from './portifolios-lista/portifolios-lista.component';

const routes: Routes = [
      //{ path: 'portifolios', redirectTo: 'portifolios/filtro/lista' },
      //{ path: 'portifolios/lista', redirectTo: 'portifolios/filtro/lista' },
      {
        path: '', component: PortifoliosComponent,
        children: [
          { path: 'filtro/lista', component: PortifolioFiltroListaComponent },
          { path: 'lista/:id', component: PortifoliosListaComponent }
        ]
      }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PortifoliosRoutingModule { }
