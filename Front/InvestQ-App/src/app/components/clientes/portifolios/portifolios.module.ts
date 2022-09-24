import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
//import { AppRoutingModule } from '@app/app-routing.module';
import { SharedModule } from '@app/shared/shared.module';
//import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { HelpersModule } from '@app/helpers/helpers.module';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { NgxCurrencyModule } from 'ngx-currency';
import { PortifoliosComponent } from './portifolios.component';
import { PortifolioFiltroListaComponent } from './portifolio-filtro-lista/portifolio-filtro-lista.component';
import { PortifoliosListaComponent } from './portifolios-lista/portifolios-lista.component';
import { PortifoliosListaAtivosComponent } from './portifolios-lista/portifolios-lista-ativos/portifolios-lista-ativos.component';
import { PortifoliosRoutingModule } from './portifolios.routing.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    NgxCurrencyModule,
    PortifoliosRoutingModule,
    ReactiveFormsModule,
    HelpersModule,
    SharedModule,
    //BrowserAnimationsModule,
    CollapseModule.forRoot(),
    TooltipModule.forRoot(),
    PaginationModule.forRoot(),
    TabsModule.forRoot(),
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-bottom-right',
      preventDuplicates: true,
      progressBar: true
    }),
    NgxSpinnerModule,
    BsDatepickerModule.forRoot()
  ],
  declarations: [
    PortifoliosComponent,
    PortifolioFiltroListaComponent,
    PortifoliosListaComponent,
    PortifoliosListaAtivosComponent
  ]
})
export class PortifoliosModule { }
