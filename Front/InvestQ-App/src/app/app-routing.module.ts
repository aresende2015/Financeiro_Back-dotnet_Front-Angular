import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PerfilComponent } from './components/user/perfil/perfil.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { UserComponent } from './components/user/user.component';
import { RegistrationComponent } from './components/user/registration/registration.component';
import { LoginComponent } from './components/user/login/login.component';
import { AuthGuard } from './guard/auth.guard';
import { HomeComponent } from './components/home/home.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      { path: 'user', redirectTo: 'user/perfil'},
      {
        path: 'user/perfil', component: PerfilComponent
      },
      { path: 'corretoras', redirectTo: 'corretoras/lista' },
      {
        path: 'corretoras',
        loadChildren: () => import('./components/corretoras/corretoras.module').then(m => m.CorretorasModule)
      },
      { path: 'lancamentos', redirectTo: 'lancamentos/filtro/lista' },
      { path: 'lancamentos/lista', redirectTo: 'lancamentos/filtro/lista' },
      { path: 'lancamentos/filtro', redirectTo: 'lancamentos/filtro/lista' },
      {
        path: 'lancamentos',
        loadChildren: () => import('./components/clientes/lancamentos/lancamentos.module').then(m => m.LancamentosModule)
      },
      { path: 'portifolios', redirectTo: 'portifolios/filtro/lista' },
      { path: 'portifolios/lista', redirectTo: 'portifolios/filtro/lista' },
      { path: 'portifolios/filtro', redirectTo: 'portifolios/filtro/lista' },
      {
        path: 'portifolios',
        loadChildren: () => import('./components/clientes/portifolios/portifolios.module').then(m => m.PortifoliosModule)
      },
      { path: 'clientes', redirectTo: 'clientes/lista' },
      {
        path: 'clientes',
        loadChildren: () => import('./components/clientes/clientes.module').then(m => m.ClientesModule)
      },
      { path: 'setores', redirectTo: 'setores/lista' },
      {
        path: 'setores',
        loadChildren: () => import('./components/ativos/acoes/setores/setores.module').then(m => m.SetoresModule)
      },
      { path: 'taxasdenegociacoes', redirectTo: 'taxasdenegociacoes/lista' },
      {
        path: 'taxasdenegociacoes',
        loadChildren: () => import('./components/ativos/taxasdenegociacoes/taxasdenegociacoes.module').then(m => m.TaxasdenegociacoesModule)
      },
      { path: 'taxasdeir', redirectTo: 'taxasdeir/lista' },
      {
        path: 'taxasdeir',
        loadChildren: () => import('./components/ativos/taxasdeir/taxasdeir.module').then(m => m.TaxasdeirModule)
      },
      { path: 'tiposdeinvestimentos', redirectTo: 'tiposdeinvestimentos/lista' },
      {
        path: 'tiposdeinvestimentos',
        loadChildren: () => import('./components/ativos/tiposdeinvestimentos/tiposdeinvestimentos.module').then(m => m.TiposdeinvestimentosModule)
      },
      { path: 'administradoresdefundosimobiliarios', redirectTo: 'administradoresdefundosimobiliarios/lista' },
      {
        path: 'administradoresdefundosimobiliarios',
        loadChildren: () => import('./components/ativos/fundosimobiliarios/administradoresdefundosimobiliarios/administradoresdefundosimobiliarios.module').then(m => m.AdministradoresdefundosimobiliariosModule)
      },
      { path: 'segmentosanbimas', redirectTo: 'segmentosanbimas/lista' },
      {
        path: 'segmentosanbimas',
        loadChildren: () => import('./components/ativos/fundosimobiliarios/segmentosanbimas/segmentosanbimas.module').then(m => m.SegmentosanbimasModule)
      },
      { path: 'tesourosdiretos', redirectTo: 'tesourosdiretos/lista' },
      {
        path: 'tesourosdiretos',
        loadChildren: () => import('./components/ativos/tesourosdiretos/tesourosdiretos.module').then(m => m.TesourosdiretosModule)
      },
      { path: 'fundosimobiliarios', redirectTo: 'fundosimobiliarios/lista' },
      {
        path: 'fundosimobiliarios',
        loadChildren: () => import('./components/ativos/fundosimobiliarios/fundosimobiliarios.module').then(m => m.FundosimobiliariosModule)
      },
      { path: 'acoes', redirectTo: 'acoes/lista' },
      {
        path: 'acoes',
        loadChildren: () => import('./components/ativos/acoes/acoes.module').then(m => m.AcoesModule)
      },
      { path: 'proventos', redirectTo: 'proventos/lista' },
      { path: 'proventos',
        loadChildren: () => import('./components/ativos/proventos/proventos.module').then(m => m.ProventosModule) // component: ContatosComponent
      },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'contatos',
        loadChildren: () => import('./components/contatos/contatos.module').then(m => m.ContatosModule) // component: ContatosComponent
      },
    ]
  },
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'login', component: LoginComponent},
      { path: 'registration', component: RegistrationComponent}
    ]
  },
  { path: 'home', component: HomeComponent },
  { path: '**', redirectTo: 'home', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
