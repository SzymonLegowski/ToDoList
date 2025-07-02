import './assets/main.css'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'
import { TodoitemsRepository } from './api/TodoitemsRepository'
import emitter from './eventBus'

const app = createApp(App)

app.use(createPinia())
app.use(router)
app.provide('TodoitemsRepository', new TodoitemsRepository(emitter));
app.mount('#app')
