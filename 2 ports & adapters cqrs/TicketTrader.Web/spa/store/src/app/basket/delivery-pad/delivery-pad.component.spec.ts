import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeliveryPadComponent } from './delivery-pad.component';

describe('DeliveryPadComponent', () => {
  let component: DeliveryPadComponent;
  let fixture: ComponentFixture<DeliveryPadComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeliveryPadComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeliveryPadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
