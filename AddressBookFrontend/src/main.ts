import { createApp } from 'vue'
import App from './App.vue'
import components from '@/components/UI/index';
import directives from '@/directives';
import { createPinia } from 'pinia'

const app = createApp(App);

components.forEach(component => {
    app.component(component.name, component)
});

directives.forEach(directive => {
    app.directive(directive.name, directive)
});

app.use(createPinia());

app.mount('#app');
