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
import { ClientesComponent } from './clientes.component';
import { ClienteDetalheComponent } from './cliente-detalhe/cliente-detalhe.component';
import { ClienteListaComponent } from './cliente-lista/cliente-lista.component';
import { ClienteCarteiraListaComponent } from './cliente-carteira-lista/cliente-carteira-lista.component';
import { ClienteCarteiraDetalheComponent } from './cliente-carteira-detalhe/cliente-carteira-detalhe.component';
import { HelpersModule } from '@app/helpers/helpers.module';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { NgxCurrencyModule } from 'ngx-currency';
import { ClientesRoutingModule } from './clientes.routing.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    NgxCurrencyModule,
    ClientesRoutingModule,
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
    ClientesComponent,
    ClienteDetalheComponent,
    ClienteListaComponent,
    ClienteCarteiraListaComponent,
    ClienteCarteiraDetalheComponent,
  ]
})
export class ClientesModule { }
