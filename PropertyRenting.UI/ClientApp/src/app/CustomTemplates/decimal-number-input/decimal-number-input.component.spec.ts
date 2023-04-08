import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DecimalNumberInputComponent } from './decimal-number-input.component';

describe('DecimalNumberInputComponent', () => {
  let component: DecimalNumberInputComponent;
  let fixture: ComponentFixture<DecimalNumberInputComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DecimalNumberInputComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DecimalNumberInputComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
