/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { TaxasdenegociacoesListaComponent } from './taxasdenegociacoes-lista.component';

describe('TaxasdenegociacoesListaComponent', () => {
  let component: TaxasdenegociacoesListaComponent;
  let fixture: ComponentFixture<TaxasdenegociacoesListaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TaxasdenegociacoesListaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TaxasdenegociacoesListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
