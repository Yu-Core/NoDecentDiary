export function previewVditor(dotNetCallbackRef, element, text, options) {
    let VditorOptions = {
        ...options,
        after: () => {
            fixLink(element);
            fixCopyDisplaySoftKeyboard(element);
            fixVideoNotDisplayFirstFrame(element);
            dotNetCallbackRef.invokeMethodAsync('After');
        }
    }
    Vditor.preview(element, text, VditorOptions);
}

export function copy(dotNetCallbackRef, callbackMethod, element) {
    const items = element.querySelectorAll('.vditor-copy');
    items.forEach(item => {
        item.addEventListener('click', function () {
            dotNetCallbackRef.invokeMethodAsync(callbackMethod);
        });
    });
}

export function previewImage(dotNetCallbackRef, callbackMethod, element) {
    const imgs = element.querySelectorAll("img");
    imgs.forEach(img => {
        img.addEventListener('click', function () {
            dotNetCallbackRef.invokeMethodAsync(callbackMethod, this.getAttribute('src'));
        });
    });
}

//�޸�������ӵ�һЩ����
function fixLink(element) {
    const links = element.querySelectorAll("a"); // ��ȡ����a��ǩ
    links.forEach(link => {
        var href = link.getAttribute('href');
        if (href && !href.includes(':')) {
            href = "https://" + href;
            link.setAttribute("href", href); // �޸�ÿ��a��ǩ��href����
        };
    });
}

function fixCopyDisplaySoftKeyboard(element) {
    const textareas = element.querySelectorAll("textarea"); // ��ȡ����textarea��ǩ
    textareas.forEach(textarea => {
        textarea.readOnly = true;
    })
}

function fixVideoNotDisplayFirstFrame(element) {
    const videos = element.querySelectorAll("video");
    videos.forEach(video => {
        video.playsinline = "true";
        if (video.hasAttribute('src')) {
            const url = new URL(video.src);
            if (!url.hash) {
                video.src += '#t=0.1';
            }

            return;
        }
        
        const sources = video.querySelectorAll('source');
        
        sources.forEach(source => {
            const url = new URL(source.src);
            if (!url.hash) {
                source.src += '#t=0.1';
            }
        });
    });
}
