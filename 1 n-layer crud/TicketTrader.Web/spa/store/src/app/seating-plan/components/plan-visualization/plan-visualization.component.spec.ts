import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PlanVisualizationComponent } from './plan-visualization.component';

describe('PlanVisualizationComponent', () => {
  let component: PlanVisualizationComponent;
  let fixture: ComponentFixture<PlanVisualizationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PlanVisualizationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PlanVisualizationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
