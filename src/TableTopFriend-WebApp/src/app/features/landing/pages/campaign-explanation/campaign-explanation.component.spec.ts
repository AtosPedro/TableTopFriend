import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CampaignExplanationComponent } from './campaign-explanation.component';

describe('CampaignExplanationComponent', () => {
  let component: CampaignExplanationComponent;
  let fixture: ComponentFixture<CampaignExplanationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CampaignExplanationComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CampaignExplanationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
