import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';
import { AppComponent } from './app.component';
import { SpinnerService } from './core/services/spinner/spinner.service';

describe('AppComponent', () => {
    let spinnerServiceSpy: jasmine.Spy;

    beforeEach(async () => {
        spinnerServiceSpy = jasmine.createSpyObj(`${SpinnerService.name}`, {}, {
            "showSpinner$": of(false)
        })

        await TestBed.configureTestingModule({
            imports: [
                RouterTestingModule
            ],
            declarations: [
                AppComponent
            ],
            providers: [
                {
                   provide: SpinnerService,
                   useValue: spinnerServiceSpy
                }
            ]
        })
        .overrideTemplate(AppComponent, '')
        .compileComponents();
    });

    it('should create the app', () => {
        const fixture = TestBed.createComponent(AppComponent);
        const app = fixture.componentInstance;
        expect(app).toBeTruthy();
    });
});
