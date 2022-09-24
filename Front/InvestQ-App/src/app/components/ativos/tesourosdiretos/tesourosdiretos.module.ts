import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxCurrencyModule } from 'ngx-currency';
import { TesourosdiretosRoutingModule } from './tesourosdiretos.routing.module';
import { HelpersModule } from '@app/helpers/helpers.module';
import { SharedModule } from '@app/shared/shared.module';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { TesourosdiretosComponent } from './tesourosdiretos.component';
import { TesourosdiretosListaComponent } from './tesourosdiretos-lista/tesourosdiretos-lista.component';
import { TesourodiretoDetalheComponent } from './tesourodireto-detalhe/tesourodireto-detalhe.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    NgxCurrencyModule,
    TesourosdiretosRoutingModule,
    ReactiveFormsModule,
    HelpersModule,
    SharedModule,
    CollapseModule.forRoot(),
    TooltipModule.forRoot(),
    PaginationModule.forRoot(),
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
    TesourosdiretosComponent,
    TesourosdiretosListaComponent,
    TesourodiretoDetalheComponent,
  ]
})
export class TesourosdiretosModule { }
