import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TicketOptionsDialogComponent } from './ticket-options-dialog.component';

describe('TicketOptionsDialogComponent', () => {
  let component: TicketOptionsDialogComponent;
  let fixture: ComponentFixture<TicketOptionsDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TicketOptionsDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TicketOptionsDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
