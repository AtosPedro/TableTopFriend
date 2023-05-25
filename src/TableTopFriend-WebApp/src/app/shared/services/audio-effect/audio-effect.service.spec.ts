import { TestBed } from '@angular/core/testing';

import { AudioEffectService } from './audio-effect.service';

describe('AudioEffectService', () => {
  let service: AudioEffectService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AudioEffectService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
