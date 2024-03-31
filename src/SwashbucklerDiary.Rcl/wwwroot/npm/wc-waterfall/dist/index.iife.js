(function(y){"use strict";var $="wc-waterfall{position:relative;display:block;box-sizing:border-box!important;overflow:unset!important}wc-waterfall>*{position:absolute;box-sizing:border-box}";const v=t=>t.nodeType==1,A=t=>{const o=t();class e extends HTMLElement{constructor(){super(),this.$props=t();const s={};for(const a in this.$props)s[a]={get:()=>this.$props[a],set:i=>{this.$props[a]=x(o,a,i),this.render()}};Object.defineProperties(this,s)}static get observedAttributes(){return Object.keys(o).filter(s=>w(o[s]))}attributeChangedCallback(s,a,i){i=x(o,s,i),this.$props[s]!==i&&(this.$props[s]=i)}}return e},x=(t,o,e)=>{const n=t[o];return w(n)?{number:s=>Number(s),string:s=>String(s),boolean:s=>s!=null&&s!="false"}[typeof n](e):void 0},w=t=>{const o=typeof t;return o=="number"||o=="string"||o=="boolean"};function I(t){let o=0;for(let e=0;e<t.length;e++)o=t[o]<=t[e]?o:e;return o}function L(t,{getW:o,setW:e,getH:n,setH:s,getPad:a,setX:i,setY:p,getChildren:d},{cols:l,gap:r}){const[c,T,W,C]=a(t),f=d(t),H=f.length;if(H){const k=(o(t)-r*(l-1)-(C+T))/l;Array.prototype.forEach.call(f,u=>e(u,k));const j=Array.prototype.map.call(f,u=>n(u)),h=Array(l).fill(c);for(let u=0;u<H;u++){const O=f[u],m=I(h);p(O,h[m]),i(O,C+(k+r)*m),h[m]+=j[u]+r}s(t,Math.max(...h)-r+W)}else s(t,c+W)}const b=Symbol(),g=Symbol();function _(t,o){let e,n,s;function a(){e=new ResizeObserver(l=>l.some(({target:r})=>r[g]!=r.offsetWidth||r[b]!=r.offsetHeight)&&d()),e.observe(t),Array.prototype.forEach.call(t.children,l=>e.observe(l)),n=new MutationObserver(l=>{l.forEach(r=>{r.addedNodes.forEach(c=>v(c)&&e.observe(c)),r.removedNodes.forEach(c=>v(c)&&e.unobserve(c))}),d()}),n.observe(t,{childList:!0,attributes:!1}),s=new MutationObserver(()=>d()),s.observe(t,{childList:!1,attributes:!0}),d()}function i(){e.disconnect(),n.disconnect(),s.disconnect()}let p=!1;function d(){p||(p=!0,requestAnimationFrame(()=>{o(),t[g]=t.offsetWidth,t[b]=t.offsetHeight,s.takeRecords(),p=!1}))}return{relayout:d,mount:a,unmount:i}}const M=(t,o)=>_(t,()=>{L(t,{getW:e=>e.offsetWidth,setW:(e,n)=>e.style.width=n+"px",getH:e=>(e[g]=e.offsetWidth,e[b]=e.offsetHeight),setH:(e,n)=>e.style.height=n+"px",getPad:e=>{const n=getComputedStyle(e);return[parseInt(n.paddingTop),parseInt(n.paddingRight),parseInt(n.paddingBottom),parseInt(n.paddingLeft)]},setX:(e,n)=>e.style.left=n+"px",setY:(e,n)=>e.style.top=n+"px",getChildren:e=>e.children},o)});document.head.appendChild(Object.assign(document.createElement("style"),{innerText:$}));const S=()=>({cols:2,gap:4});class E extends A(S){constructor(){super()}connectedCallback(){this._layout=M(this,this),this._layout.mount()}disconnectedCallback(){this._layout.unmount()}render(){this.isConnected&&this._layout.relayout()}}return customElements.define("wc-waterfall",E),y.WaterfallElement=E,y})({});