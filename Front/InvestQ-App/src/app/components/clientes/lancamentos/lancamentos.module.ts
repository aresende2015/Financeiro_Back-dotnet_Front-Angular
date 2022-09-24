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
import { LancamentosComponent } from './lancamentos.component';
import { LancamentosListaComponent } from './lancamentos-lista/lancamentos-lista.component';
import { LancamentoDetalheComponent } from './lancamento-detalhe/lancamento-detalhe.component';
import { LancamentoFiltroListaComponent } from './lancamento-filtro-lista/lancamento-filtro-lista.component';
import { LancamentosRoutingModule } from './lancamentos.routing.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    NgxCurrencyModule,
    LancamentosRoutingModule,
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
    LancamentosComponent,
    LancamentosListaComponent,
    LancamentoDetalheComponent,
    LancamentoFiltroListaComponent,
  ]
})
export class LancamentosModule { }
