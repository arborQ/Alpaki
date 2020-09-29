import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DreamDetailsComponent } from './dream-details.component';

describe('DreamDetailsComponent', () => {
  let component: DreamDetailsComponent;
  let fixture: ComponentFixture<DreamDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DreamDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DreamDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
