/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { CorretorasComponent } from './corretoras.component';

describe('CorretorasComponent', () => {
  let component: CorretorasComponent;
  let fixture: ComponentFixture<CorretorasComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CorretorasComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CorretorasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
