import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReservationsPadComponent } from './reservations-pad.component';

describe('ReservationsPadComponent', () => {
  let component: ReservationsPadComponent;
  let fixture: ComponentFixture<ReservationsPadComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReservationsPadComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReservationsPadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
