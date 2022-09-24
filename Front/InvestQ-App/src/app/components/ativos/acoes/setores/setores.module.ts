import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxCurrencyModule } from 'ngx-currency';
import { SetoresRoutingModule } from './setores.routing.module';
import { HelpersModule } from '@app/helpers/helpers.module';
import { SharedModule } from '@app/shared/shared.module';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ToastrModule } from 'ngx-toastr';
import { NgxSpinnerModule } from 'ngx-spinner';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { SetoresComponent } from './setores.component';
import { SetorDetalheComponent } from './setor-detalhe/setor-detalhe.component';
import { SetorListaComponent } from './setor-lista/setor-lista.component';
import { SubsetoresListaComponent } from './subsetores-lista/subsetores-lista.component';
import { SubsetorDetalheComponent } from './subsetor-detalhe/subsetor-detalhe.component';
import { SegmentosListaComponent } from './segmentos-lista/segmentos-lista.component';
import { SegmentoDetalheComponent } from './segmento-detalhe/segmento-detalhe.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    NgxCurrencyModule,
    SetoresRoutingModule,
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
    SetoresComponent,
    SetorDetalheComponent,
    SetorListaComponent,
    SubsetoresListaComponent,
    SubsetorDetalheComponent,
    SegmentosListaComponent,
    SegmentoDetalheComponent,
  ]
})
export class SetoresModule { }
