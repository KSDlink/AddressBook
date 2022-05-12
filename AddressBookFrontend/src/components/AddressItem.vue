<template>
  <div class='card my-3'>
    <h5 class='card-header'>{{ props.contact.lastName }} {{ contact.firstName }}</h5>
    <div class='card-body'>
      <h5 class='card-title'>Номер: {{ props.contact.phoneNumber }} E-mail: {{ props.contact.email }}</h5>
      <p class='card-text'>Пол: {{ props.contact.gender }} Дата рождения: {{ props.contact.birth }}</p>
      <div class='btn-group'>
        <custom-button class='btn btn-warning' @click='updateContact'>Изменить</custom-button>
        <custom-button class='btn btn-danger' @click='removeContact'>Удалить</custom-button>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
export default {
    name: 'address-item'
}
</script>

<script setup lang="ts">
import { defineProps, defineEmits } from 'vue';

const props = defineProps({
  contact: {
    type: Object,
    required: true,
  }
});

const emit = defineEmits(['removeContact', 'updateContact']);

const removeContact = () => {
  emit('removeContact', props.contact);
}

const updateContact = () => {
  emit('updateContact', props.contact);
}

</script>

<style scoped>
  /* .contact {
    padding: 15px;
    border: 2px solid teal;
    margin-top: 15px;
    align-items: center;
    justify-content: space-between;
  }
  .contact__btns {
    margin: 15px 10px;
  } */
</style>
