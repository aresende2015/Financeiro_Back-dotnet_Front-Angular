/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TaxasdenegociacoesDetalheComponent } from './taxasdenegociacoes-detalhe.component';

describe('TaxasdenegociacoesDetalheComponent', () => {
  let component: TaxasdenegociacoesDetalheComponent;
  let fixture: ComponentFixture<TaxasdenegociacoesDetalheComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TaxasdenegociacoesDetalheComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TaxasdenegociacoesDetalheComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
