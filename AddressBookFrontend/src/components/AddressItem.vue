<template>
  <div class='card my-3'>
    <h5 class='card-header'>{{ contact.lastName }} {{ contact.firstName }}</h5>
    <div class='card-body'>
      <h5 class='card-title'>Номер: {{ contact.phoneNumber }} E-mail: {{ contact.email }}</h5>
      <p class='card-text'>Пол: {{ contact.gender }} Дата рождения: {{ contact.birth }}</p>
      <div class='btn-group'>
        <custom-button class='btn btn-warning' @click='updateContact'>Изменить</custom-button>
        <custom-button class='btn btn-danger' @click='removeContact'>Удалить</custom-button>
      </div>
    </div>
  </div>
</template>

<script>
import { defineComponent } from 'vue';

export default defineComponent ({
  name: 'address-item',
  props: {
    contact: {
      type: Object,
      required: true,
    }
  },
  methods: {
    removeContact(){
      this.$emit('removeContact', this.contact);
    },
    updateContact(){
      this.$emit('updateContact', this.contact);
    }
  }
});
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
