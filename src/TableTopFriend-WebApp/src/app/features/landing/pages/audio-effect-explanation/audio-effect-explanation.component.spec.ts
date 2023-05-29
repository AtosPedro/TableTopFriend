import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AudioEffectExplanationComponent } from './audio-effect-explanation.component';

describe('AudioEffectExplanationComponent', () => {
  let component: AudioEffectExplanationComponent;
  let fixture: ComponentFixture<AudioEffectExplanationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AudioEffectExplanationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AudioEffectExplanationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
