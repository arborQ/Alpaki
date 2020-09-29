import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DreamCardComponent } from './dream-card.component';

describe('DreamCardComponent', () => {
  let component: DreamCardComponent;
  let fixture: ComponentFixture<DreamCardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DreamCardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DreamCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
