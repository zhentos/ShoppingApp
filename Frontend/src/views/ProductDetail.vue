<template>
  <v-btn
    @click="router.push({ name: 'Catalog' })"
    color="primary"
    variant="elevated"
  >
    Back to catalog
  </v-btn>

  <div class="product">
    <h2 class="category">{{ selectedProduct.category }}</h2>
    <p class="title text-grey">{{ selectedProduct.title }}</p>
    <h2 class="price">${{ selectedProduct.price }}</h2>
    
    <div class="product-image">
      <img :src="selectedProduct.thumbnail" alt="">
    </div>

    <b class="text-grey">Description</b>
    <p class="description text-grey">{{ selectedProduct.description }}</p>

    <v-btn
      variant="flat"
      class="custom-button"
      @click="addToCart"
    >
      Add to Cart
    </v-btn>
  </div>
</template>


<script>
  import "@/assets/styles.scss";

  import { defineComponent } from "vue";
  export default defineComponent({
    name: 'ProductDetails'
  })
</script>

<script setup>
  import { computed } from "vue";
  import { productsStore } from "@/stores/products";
  import { useRoute, useRouter } from "vue-router";

  const store = productsStore()
  const router = useRouter()
  const route = useRoute()

  const selectedProduct = computed(() => {
    return store.products.find((item) => item.id === Number(route.params.id))
  })

  const addToCart = () => {
    store.addToCart(selectedProduct.value)
    router.push({ name: 'CartView' })
  }
</script>

<style scoped>
.product {
  display: flex;
  flex-direction: column; /* Stack items vertically */
  align-items: flex-start; /* Align items to the left */
  margin-top: 50px;
}

.product-image {
  display: flex; /* Use flexbox for centering */
  justify-content: center; /* Center the image horizontally */
  width: 100%; /* Ensure it takes full width of container */
  margin: 16px 0; /* Add margin for spacing */
}

.product-image img {
  max-width: 100%; /* Make sure image is responsive */
  height: auto; /* Maintain aspect ratio */
}

.custom-button {
    background-color: #141718; 
    color: white; 
    text-transform: none;
}
 
.category,
.title,
.price,
.description {
  margin-bottom: 16px; /* Add gap between rows */
}

p.description {
  margin-top: 10px;
}
</style>