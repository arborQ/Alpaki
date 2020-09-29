import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DreamProgressComponent } from './dream-progress.component';

describe('DreamProgressComponent', () => {
  let component: DreamProgressComponent;
  let fixture: ComponentFixture<DreamProgressComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DreamProgressComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DreamProgressComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
