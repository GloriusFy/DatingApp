import { Directive, HostBinding, HostListener, Input } from '@angular/core';

@Directive({
    selector: 'img[daImageWithFallback]',
    host: {
        "[src]": "src"
    }
})
export class ImageWithFallbackDirective {
    @Input()
    public src: string | undefined;

    @HostListener('error')
    public substituteWithFallback(): void {
        this.src = "assets/user-fallback.png";
    }
}
