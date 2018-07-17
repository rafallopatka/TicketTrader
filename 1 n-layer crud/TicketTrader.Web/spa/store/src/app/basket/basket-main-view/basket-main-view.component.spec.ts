import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BasketMainViewComponent } from './basket-main-view.component';

describe('BasketMainViewComponent', () => {
  let component: BasketMainViewComponent;
  let fixture: ComponentFixture<BasketMainViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BasketMainViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BasketMainViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
