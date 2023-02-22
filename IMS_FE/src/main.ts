import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'

import './assets/main.css'

import PrimeVue from 'primevue/config';
import Dialog from 'primevue/dialog';

import "bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";
import { library } from "@fortawesome/fontawesome-svg-core";
import { faPhone, faUser, faFlag } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";

library.add(faPhone, faUser, faFlag);


const app = createApp(App)

app.use(createPinia())
app.use(router)
app.use(PrimeVue);

app.component('font-awesome-icon', FontAwesomeIcon);
app.component('Dialog', Dialog);
app.mount('#app')
