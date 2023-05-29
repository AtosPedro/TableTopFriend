import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CharacterExplanationComponent } from './character-explanation.component';

describe('CharacterExplanationComponent', () => {
  let component: CharacterExplanationComponent;
  let fixture: ComponentFixture<CharacterExplanationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CharacterExplanationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CharacterExplanationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
