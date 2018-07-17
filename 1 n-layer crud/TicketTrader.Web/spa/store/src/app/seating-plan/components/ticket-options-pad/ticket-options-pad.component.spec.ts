import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketOptionsPadComponent } from './ticket-options-pad.component';

describe('TicketOptionsPadComponent', () => {
  let component: TicketOptionsPadComponent;
  let fixture: ComponentFixture<TicketOptionsPadComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TicketOptionsPadComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TicketOptionsPadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
