import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ContatosComponent } from './contatos.component';
import { SharedModule } from '@app/shared/shared.module';
import { ContatosRoutingModule } from './contatos.routing.module';

@NgModule({
  imports: [
    CommonModule,
    ContatosRoutingModule,
    SharedModule
  ],
  declarations: [
    ContatosComponent,
  ]
})
export class ContatosModule { }
