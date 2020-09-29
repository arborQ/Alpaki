import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DreamListItemComponent } from './dream-list-item.component';

describe('DreamListItemComponent', () => {
  let component: DreamListItemComponent;
  let fixture: ComponentFixture<DreamListItemComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DreamListItemComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DreamListItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
