import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SeatingPlanMainViewComponent } from './seating-plan-main-view.component';

describe('SeatingPlanMainViewComponent', () => {
  let component: SeatingPlanMainViewComponent;
  let fixture: ComponentFixture<SeatingPlanMainViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SeatingPlanMainViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SeatingPlanMainViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
