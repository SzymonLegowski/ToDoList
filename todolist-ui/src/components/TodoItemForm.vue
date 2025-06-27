<template>
  <div class="form-wrapper">
    <h2 class="form-title">{{ isEditMode ? 'Edytuj zadanie' : 'Dodaj nowe zadanie' }}</h2>
    <form @submit.prevent="handleSubmit" class="todo-form">
      <div class="form-group">
        <label for="title">Tytuł</label>
        <input type="text" id="title" v-model="form.title" required />
      </div>

      <div class="form-group">
        <label for="description">Opis</label>
        <textarea id="description" v-model="form.description" rows="3" required></textarea>
      </div>

      <div class="form-group">
        <label for="date">Data</label>
        <input type="date" id="date" v-model="form.date" required />
      </div>

      <div class="form-actions">
        <button type="submit" class="submit-btn">
          {{ isEditMode ? 'Zapisz zmiany' : 'Dodaj zadanie' }}
        </button>
        <button type="button" class="cancel-btn" @click="$emit('cancel')">Powrót</button>
      </div>
    </form>
  </div>
</template>

<script setup>
import { ref, watch, defineProps, defineEmits } from 'vue';

const props = defineProps({
  todoitem: {
    type: Object,
    default: () => ({ title: '', description: '', date: '' })
  },
  isEditMode: {
    type: Boolean,
    default: false
  }
});

const emit = defineEmits(['submit', 'cancel']);

const form = ref({ ...props.todoitem });

watch(
  () => props.todoitem,
  (newVal) => {
    form.value = { ...newVal };
  },
  { deep: true }
);

function handleSubmit() {
  emit('submit', form.value);
}
</script>

<style scoped>
.form-wrapper {
  max-width: 500px;
  margin: auto;
  background-color: #f9fafb;
  padding: 24px;
  border-radius: 12px;
  box-shadow: 0 4px 16px rgba(0,0,0,0.1);
}

.form-title {
  text-align: center;
  font-size: 22px;
  font-weight: bold;
  margin-bottom: 20px;
}

.todo-form {
  display: grid;
  gap: 16px;
}

.form-group {
  display: flex;
  flex-direction: column;
}

label {
  font-weight: 600;
  margin-bottom: 6px;
  color: #374151;
}

input,
textarea {
  padding: 10px;
  font-size: 15px;
  border: 1px solid #d1d5db;
  border-radius: 6px;
  transition: border-color 0.2s;
}

input:focus,
textarea:focus {
  border-color: #3b82f6;
  outline: none;
}

.form-actions {
  display: flex;
  justify-content: space-between;
  gap: 12px;
}

.submit-btn,
.cancel-btn {
  padding: 10px 16px;
  font-size: 15px;
  font-weight: 600;
  border: none;
  border-radius: 6px;
  cursor: pointer;
  transition: 0.2s;
}

.submit-btn {
  background-color: #3b82f6;
  color: white;
}

.submit-btn:hover {
  background-color: #2563eb;
}

.cancel-btn {
  background-color: #e5e7eb;
  color: #374151;
}

.cancel-btn:hover {
  background-color: #d1d5db;
}
</style>
