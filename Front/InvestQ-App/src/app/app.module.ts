import { DEFAULT_CURRENCY_CODE, LOCALE_ID, CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import ptBr from '@angular/common/locales/pt'
import { registerLocaleData } from '@angular/common';

import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';

import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { NgxCurrencyModule } from 'ngx-currency';

import { HelpersModule } from './helpers/helpers.module';
import { UserModule } from './components/user/user.module';
import { AuthGuard } from './guard/auth.guard';
import { HomeModule } from './components/home/home.module';
import { DashboardModule } from './components/dashboard/dashboard.module';
import { AppRoutingModule } from './app-routing.module';

import { JwtInterceptor } from './interceptors/jwt.interceptor';

import { AppComponent } from './app.component';

import { NavComponent } from './shared/nav/nav.component';

import { CorretoraService } from './services/corretoras/corretora.service';
import { ClienteService } from './services/clientes/cliente.service';
import { SetorService } from './services/ativos/setor.service';
import { SubsetorService } from './services/ativos/subsetor.service';
import { SegmentoService } from './services/ativos/segmento.service';
import { UserService } from './services/user.service';
import { TipodeinvestimentoService } from './services/ativos/tipodeinvestimento.service';
import { TesourodiretoService } from './services/ativos/tesourodireto.service';
import { AtivoService } from './services/ativos/ativo.service';
import { AdministradordefundoimobiliarioService } from './services/ativos/administradordefundoimobiliario.service';
import { SegmentoanbimaService } from './services/ativos/segmentoanbima.service';
import { FundoimobiliarioService } from './services/ativos/fundoimobiliario.service';
import { AcaoService } from './services/ativos/acao.service';
import { ProventoService } from './services/ativos/provento.service';
import { CarteiraService } from './services/clientes/carteira.service';
import { LancamentoService } from './services/clientes/lancamento.service';
import { PortifolioService } from './services/clientes/portifolio.service';
import { TaxadenegociacaoService } from './services/ativos/taxadenegociacao.service';
import { TaxadeirService } from './services/ativos/taxadeir.service';

defineLocale('pt-br', ptBrLocale);
registerLocaleData(ptBr);
@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
   ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    NgxCurrencyModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    CollapseModule.forRoot(),
    TooltipModule.forRoot(),
    BsDropdownModule.forRoot(),
    ModalModule.forRoot(),
    PaginationModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true
    }),
    NgxSpinnerModule,
    BsDatepickerModule.forRoot(),
    HelpersModule,
    UserModule,
    HomeModule,
    DashboardModule,
    AppRoutingModule,
  ],
  providers: [
    CorretoraService,
    ClienteService,
    LancamentoService,
    PortifolioService,
    CarteiraService,
    SetorService,
    SubsetorService,
    SegmentoService,
    AcaoService,
    TipodeinvestimentoService,
    AdministradordefundoimobiliarioService,
    ProventoService,
    FundoimobiliarioService,
    SegmentoanbimaService,
    TesourodiretoService,
    AtivoService,
    TaxadenegociacaoService,
    TaxadeirService,
    UserService,
    AuthGuard,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: LOCALE_ID, useValue: 'pt-BR' },
    { provide: DEFAULT_CURRENCY_CODE, useValue: 'BRL' },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
