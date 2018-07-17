import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PaymentPadComponent } from './payment-pad.component';

describe('PaymentPadComponent', () => {
  let component: PaymentPadComponent;
  let fixture: ComponentFixture<PaymentPadComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PaymentPadComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PaymentPadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
