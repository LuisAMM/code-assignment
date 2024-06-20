import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CurrencyConverterPageComponent } from './currency-converter-page.component';

describe('CurrencyConverterPageComponent', () => {
  let component: CurrencyConverterPageComponent;
  let fixture: ComponentFixture<CurrencyConverterPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CurrencyConverterPageComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CurrencyConverterPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
